using SurveyBasket.Api.Contracts.Question;

namespace SurveyBasket.Api.Services;

public class QuestionServices(ApplicationDbContext _dbContext) : IQuestionServices
{
    public async Task<AppResponse<List<QuestionResponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken = default)
    {
        var questions = await _dbContext.Questions
            .Where(i => i.PollId == pollId && i.IsActive)
            .Include(i => i.Answers.Where(i => i.IsActive))
            .Select(i => i.Adapt<QuestionResponse>())
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return AppResponse.Success(questions, SharedMessage.Success);
    }

    public async Task<AppResponse<QuestionResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var question = await _dbContext.Questions.Include(i => i.Answers.Where(i => i.IsActive)).SingleOrDefaultAsync(i => i.Id == id, cancellationToken);
        if (question is null)
            return AppResponse.Failure<QuestionResponse>(error: QuestionError.QUESTION_NOT_FOUND);

        return AppResponse.Success(question.Adapt<QuestionResponse>(), SharedMessage.Success);
    }

    public async Task<AppResponse<QuestionResponse>> AddAsync(int pollId, QuestionRequest questionRequest, CancellationToken cancellationToken = default)
    {
        bool pollIsExist = await _dbContext.Polls.AnyAsync(i => i.Id == pollId, cancellationToken);
        if (pollIsExist is false)
            return AppResponse.Failure<QuestionResponse>(error: PollError.POLL_NOT_FOUND);

        bool questionIsExist = await _dbContext.Questions.AnyAsync(i => i.Id == pollId && i.Content == questionRequest.Content, cancellationToken);
        if (questionIsExist is true)
            return AppResponse.Failure<QuestionResponse>(error: QuestionError.DUBLICATE_QUESTION);

        Question question = questionRequest.Adapt<Question>();
        question.PollId = pollId;

        questionRequest.Answers.ForEach(answer => question.Answers.Add(new Answer() { Content = answer }));

        await _dbContext.Questions.AddAsync(question, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return AppResponse.Success(question.Adapt<QuestionResponse>(), SharedMessage.Success);
    }

    public async Task<AppResponse> UpdateAsync(int id, QuestionRequest questionRequest, CancellationToken cancellationToken = default)
    {
        bool questionIsExist = await _dbContext.Questions.AnyAsync(i => i.Id == id && i.PollId == questionRequest.PollId, cancellationToken);
        if (questionIsExist is false)
            return AppResponse.Failure(error: QuestionError.QUESTION_NOT_FOUND);

        var question = await _dbContext.Questions.Include(i => i.Answers).SingleOrDefaultAsync(i => i.Id == id && i.PollId == questionRequest.PollId, cancellationToken);

        var oldAnswers = question!.Answers // To Update
            .Select(i => i.Content)
            .ToList();

        var newAnswers = questionRequest.Answers // To Add
            .Except(oldAnswers).ToList();

        var deletedAnswers = oldAnswers // To Delete
            .Except(questionRequest.Answers).ToList();

        question.Answers.ToList().ForEach(answer =>
        {
            answer.IsActive = !deletedAnswers.Contains(answer.Content);
        });

        newAnswers.ForEach(answer => question.Answers.Add(new Answer() { Content = answer })); // To Add

        _dbContext.Questions.Update(question);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return AppResponse.Success(SharedMessage.Success);

    }
}

﻿using Movies.DataAccess.Models;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IQuizSessionService
    {
        // Creates a new quiz session for a quiz group.
        Task<QuizSession> CreateQuizSessionAsync(CreateQuizSessionDto quizSession);

        // Retrieves a quiz session by its ID.
        Task<QuizSession> GetQuizSessionByIdAsync(int quizSessionId);

        // Retrieves all quiz sessions for a given quiz group.
        Task<IEnumerable<QuizSession>> GetQuizSessionsForGroupQuizAsync(int groupQuizId);
    }
}

// <copyright file="Result.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApplication1.Shared
{
    /// <summary>
    /// Универсальный результат выполнения операции.
    /// </summary>
    /// <typeparam name="T">Тип значения.</typeparam>
    public class Result<T>
    {
        protected Result(bool isSuccess, T? value, string? error)
        {
            this.IsSuccess = isSuccess;
            this.Value = value;
            this.Error = error;
        }

        public bool IsSuccess { get; }

        public T? Value { get; }

        public string? Error { get; }

        public static Result<T> Success(T value) => new(true, value, null);

        public static Result<T> Failure(string error) => new(false, default, error);
    }
}

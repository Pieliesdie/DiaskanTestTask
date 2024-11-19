﻿namespace TaskManager.Common.Result.Models;

public sealed class Result<TValue> : Result
{
    private readonly TValue? value;

    private Result(TValue value) : base()
    {
        this.value = value;
    }

    private Result(Error error) : base(error)
    {
        value = default;
    }

    public TValue Value => IsSuccess ? value! : throw new InvalidOperationException("Value can not be accessed when IsSuccess is false");

    public static implicit operator Result<TValue>(Error error) => new(error);

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static Result<TValue> Success(TValue value) => new(value);

    public static new Result<TValue> Failure(Error error) => new(error);
}

﻿using TaskManager.Common.Result.Models;

namespace TaskManager.Common.Result;

public static class ResultExtensions
{
    public static T Match<T>(
        this Models.Result result,
        Func<T> onSuccess,
        Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error!);
    }

    public static T Match<T, TValue>(
        this Result<TValue> result,
        Func<TValue, T> onSuccess,
        Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error!);
    }
}
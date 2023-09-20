package com.worldpath.community.action.interfaces;

public interface Action<I, O> {
    O execute(I input);
}

﻿namespace MovieCharactersAPI.Exceptions
{
    public class CharacterNotFoundException: Exception
    {
        public CharacterNotFoundException(string message) : base(message) { }
    }
}

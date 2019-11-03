using System;
using System.Collections.Generic;
using System.Text;

namespace entities
{
    public interface IResult
    {
        string[] Data { get; }
        string[] Errors { get; }
    }
}

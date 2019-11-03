using entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class Result : IResult
    {
        public string[] Data { get; set; }

        public string[] Errors { get; set; }
    }
}

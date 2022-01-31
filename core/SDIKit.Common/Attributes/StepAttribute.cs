using System;

namespace SDIKit.Common.Attributes
{
    public class StepAttribute : Attribute
    {
        public int Step { get; set; }

        public StepAttribute(int step)
        {
            Step = step;
        }
    }
}
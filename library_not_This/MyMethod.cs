using System;
using System.Collections.Generic;

namespace dosomething
{
    internal class MyMethod<T>
    {
        public static List<T> merge(List<T> List_One, List<T> List_Two)
        {
            if (List_One.Count == 0) return List_Two;
            else if
                (List_Two.Count == 0) return List_One;

            int index = 0;
            if (List_One.Count >= List_Two.Count)
            {
                while (List_Two.Count > index)
                {
                    if (!List_One.Contains(List_Two[index]))
                    {
                        List_One.Add(List_Two[index]);
                    }
                    index++;
                }
                return List_One;
            }
            else
                return merge(List_Two, List_One);
        }
    }
}
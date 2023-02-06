using System;

namespace TagValidator;
public class TagValidator
{
    Stack<string> stack = new Stack<string>();
    bool contains_tag = false;

    /// <summary>
    /// Check if a string is a valid XML string.
    /// </summary>
    /// <param name="xml">A XML string to be validated</param>
    /// <returns>If the string is a valid XML string, return true. Otherwise, return false</returns>
    public bool DetermineXml(string xml)
    {
        if (xml[0] != '<' || xml[xml.Length - 1] != '>')
        {
            return false;
        }

        for (int i = 0; i < xml.Length; i++)
        {
            int endIndex;
            bool endTag = false;
            if (stack.Count == 0 && contains_tag)
            {
                return false;
            }
            if (xml[i] == '<')
            {
                if (stack.Count != 0 && xml[i + 1] == '!')
                {
                    endIndex = xml.IndexOf("]]>", i + 1);
                    if (endIndex < 0 || !IsValidCDATA(xml.Substring(i + 2, (endIndex - i - 2))))
                    {
                        return false;
                    }
                }
                else
                {
                    if (xml[i + 1] == '/')
                    {
                        endTag = true;
                        i++;
                    }
                    endIndex = xml.IndexOf(">", i + 1);
                    if (endIndex < 0 || !IsValidTagName(xml.Substring(i + 1, (endIndex - i - 1))) || !IsElementMatched(xml.Substring(i + 1, (endIndex - i - 1)), endTag))
                    {
                        return false;
                    }

                }
                i = endIndex;
            }
        }
        return stack.Count == 0 && contains_tag;

    }

    /// <summary>
    /// Getter method that allows you to access the stack in TagValidator class.
    /// </summary>
    /// <returns>The stack in the TagValidator class</returns>
    public Stack<string> Stack
    {
        get
        {
            return stack;
        }
    }

    /// <summary>
    /// Check if a string is valid CDATA.
    /// </summary>
    /// <param name="str">A string to be validated</param>
    /// <returns>If the string is a valid CDATA, return true. Otherwise, return false</returns>
    public bool IsValidCDATA(string str)
    {
        return str.IndexOf("[CDATA[") == 0;
    }

    /// <summary>
    /// Check if a string is a valid tag name.
    /// </summary>
    /// <param name="str">A string to be validated</param>
    /// <returns>If the string is a valid tag name, return true. Otherwise, return false</returns>
    public bool IsValidTagName(string str)
    {
        if (str.Length < 1)
        {
            return false;
        }
        if (str[0] != '_' && !Char.IsLetter(str[0]))
        {
            return false;
        }
        if (str.Length >= 3 && str.Substring(0, 3).ToLower().Equals("xml"))
        {
            return false;
        }
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == ' ')
            {
                return false;
            }
        }
        return true;

    }

    /// <summary>
    /// Check if a string of tag name is balanced-match.
    /// </summary>
    /// <param name="str">A string to be validated</param>
    /// <param name="endTag">A boolean indicating if the given tag name is an end tag</param>
    /// <returns>If the string of tag name is balanced-match, return true. Otherwise, return false</returns>
    public bool IsElementMatched(string str, bool endTag)
    {
        if (endTag)
        {
            if (stack.Count != 0 && stack.Peek().Equals(str))
            {
                stack.Pop();
            }
            else
            {
                return false;
            }
        }
        else
        {
            stack.Push(str);
            contains_tag = true;

        }
        return true;
    }
}

﻿namespace PrepSharp.Consts
{
    public static class RegexPatterns
    {
        public const string Password = @"(?=(.*[0-9]))(?=.*[\!@#$%^&*()\\[\]{}\-_+=~`|:;""'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";
        public const string Username = @"^[a-zA-Z0-9-._@]*$";
        public const string NumbersOnly = @"^[0-9]*$";
        public const string CharactersOnly_Eng = "^[a-zA-Z-_ ]*$";
        public const string CharactersOnly_EngAndDotAndAmpersand = "^[a-zA-Z-_. &]*$";
        public const string CharactersOnly_Ar = "^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF ]*$";
        public const string NumbersAndChrOnly_ArEng = "^(?=.*[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z])[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9 _-]+$";
        public const string DenySpecialCharacters = "^[^<>!#%$]*$";
        public const string MobileNumber = "^01[0,1,2,5]{1}[0-9]{8}$"; // 010, 011, 012, 015 only
        public const string NationalId = "^[2,3]{1}[0-9]{13}$"; // 14 digits, starts with 2 or 3
    }
}

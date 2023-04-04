using System;
using System.Collections.Generic;
using URLShortener.BLL.Services.Interfaces;

namespace URLShortener.BLL.Services.Realizations
{
    public class ShortenUrlService : IShortenUrlService
    {
        private Dictionary<string, string> shortToLong = new Dictionary<string, string>();
        private Dictionary<string, string> longToShort = new Dictionary<string, string>();
        private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private Random rand = new Random();

        public string ShortenUrl(string longUrl)
        {
            if (longToShort.ContainsKey(longUrl))
            {
                return longToShort[longUrl];
            }

            string shortUrl;
            do
            {
                char[] shortUrlChars = new char[6];
                for (int i = 0; i < shortUrlChars.Length; i++)
                {
                    shortUrlChars[i] = chars[rand.Next(chars.Length)];
                }
                shortUrl = new string(shortUrlChars);
            } while (shortToLong.ContainsKey(shortUrl));

            return shortUrl;
        }
    }
}
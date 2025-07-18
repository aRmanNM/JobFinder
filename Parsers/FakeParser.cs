using System.Collections.Generic;
using System.Threading.Tasks;
using JobFinder.Models;

namespace JobFinder.Parsers
{
    public class FakeParser : IParser
    {
        public string Name => "Fake";

        public List<JobAd> GetJobAds(QueryUrl url)
        {
            var delay = Task.Delay(1000 * 2);
            delay.Wait();

            return new List<JobAd>
            {
                new JobAd
                {
                    Title = "استخدام توسعه‌دهنده Front-end React",
                    Url = "https://quera.ir/magnet/jobs/dk7kv",
                    Description = null,
                    Company = "شرکت اطلس راهبر دریا",
                    LogoUrl = "https://quera.ir/media/CACHE/images/public/careers/logos/f75221f6b1104022a02ae32fd2ce104a/ef44a1479cb53bfec54c50451b6b6635.png",
                    Location = "تهران",
                    Experience = null,
                    DateIdentifier = "۶ روز پیش",
                },
                new JobAd
                {
                    Title = "استخدام برنامه‌نویس ++Embedded system C/C",
                    Url = "https://quera.ir/magnet/jobs/wxp7v",
                    Description = null,
                    Company = "پیشروابتکارودانش",
                    LogoUrl = "https://quera.ir/media/CACHE/images/public/careers/logos/2737f2f8bfa747aba754094fda9af348/ddec892436f9dfecf981a837879245de.png",
                    Location = "شیراز",
                    Experience = null,
                    DateIdentifier = "۷ روز پیش",
                },
                new JobAd
                {
                    Title = "استخدام برنامه‌نویس Full Stack",
                    Url = "https://quera.ir/magnet/jobs/gkx9x",
                    Description = null,
                    Company = "مهندسی ارتباطی پیام پرداز",
                    LogoUrl = "https://quera.ir/media/CACHE/images/public/careers/logos/14133168c38f42b386192f1247b04a8d/5a45e086a3787cb6e4eb2cdb88aff686.png",
                    Location = "اصفهان",
                    Experience = null,
                    DateIdentifier = "۱۷ روز پیش",
                },
                new JobAd
                {
                    Title = "استخدام کارآموز مهندسی نرم‌افزار",
                    Url = "https://quera.ir/magnet/jobs/k98qx",
                    Description = null,
                    Company = "Ideapardazan afsoon",
                    LogoUrl = "https://quera.ir/media/CACHE/images/public/careers/logos/5b58838b188746d789e8f92f079497ca/c791a9367ef7667d8404c1e0f56fad41.png",
                    Location = null,
                    Experience = null,
                    DateIdentifier = "۲۰ روز پیش",
                }
            };
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using JobFinder.Models;

namespace JobFinder.Parsers
{
    public class FakeParser : IParser
    {
        public string Name => "Fake";

        public async Task<List<JobAd>> GetJobAds(QueryUrl url)
        {
            var delay = Task.Delay(1000 * 2);
            delay.Wait();

            return await Task.FromResult(new List<JobAd>
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
            });
        }

        public async Task<string> GetJobDescription(string url)
        {
            return await Task.FromResult(
@"
    دسته‌بندی شغلی
    وب،‌ برنامه‌نویسی و نرم‌افزار
    موقعیت مکانی
    تهران ، تهران
    نوع همکاری
    تمام وقت
    حداقل سابقه کار
    سه تا شش سال
    حقوق
    توافقی

شرح موقعیت شغلی
شرکت ما در حوزه فناوری و ارائه خدمات نرم‌افزاری حوزه بیمه به دنبال یک برنامه‌نویس Back-End حرفه‌ای با تجربه در .NET می‌باشد. اگر شما فردی متخصص در توسعه نرم‌افزارهای مقیاس‌پذیر و قابل اعتماد هستید و علاقه‌مند به پیوستن به یک تیم پویا و چابک، ما منتظر شما هستیم.



مسئولیت‌ها:


    طراحی و توسعه‌ی اپلیکیشن‌های بک‌اند با استفاده از dotNet و SQL Server
    پیاده‌سازی و مدیریت سرویس‌ها با استفاده از معماری مناسب
    پیاده‌سازی و بهینه‌سازی APIهای RESTful
    همکاری با تیم‌های دیگر (فرانت‌اند، QA و...) برای ارائه‌ی راه‌حل‌های کارآمد
    نظارت و بهینه‌سازی عملکرد اپلیکیشن‌ها
    به‌روزرسانی مستندات فنی و رفع مشکلات کد

مهارت‌ها و توانایی‌ها:


    تسلط کامل به .NET Core
    تجربه کار با معماری های توسعه پذیر و ابزارهای مرتبط مانند Docker و Kubernetes
    آشنایی با اصول طراحی پایگاه داده‌ها و تجربه کار با SQL Server
    تجربه در پیاده‌سازی معماری‌های لایه‌ای مانند Clean Architecture
    آشنایی با ابزارهای مدیریت پروژه مانند Azure Devops و سیستم‌های کنترل نسخه (Git)
    آشنایی با اصول DevOps و تجربه کار با CI/CD امتیاز محسوب می‌شود
    توانایی حل مسائل و تفکر تحلیلی قوی

مزایای شغلی:


    بیمه تکمیلی
    هدایای مناسبتی
    کمک هزینه آموزشی و ورزشی

معرفی شرکت
شرکت توسعه انفورماتیک دی به عنوان یکی از شرکت‌های فعال در حوزه اینشورتک و خدمات مالی، در حوزه تولید و پشتیبانی سامانه‌ها و پلت‌فرم‌های نوین بیمه‌ای و بانکی در حال فعالیت است.
این شرکت با هدف ارائه خدمات کاربردی و نوین به شرکت‌ها و مشتریان صنعت بیمه سعی در ایجاد تحول در حوزه بیمه‌گری، ارزیابی و ارائه خدمات نوین به مشتریان و ذی‌نفعان این صنعت را دارد.

    مهارت‌های مورد نیاز
    Back-end .NET Core Sql Server
    جنسیت
    مهم نیست
    وضعیت نظام وظیفه
    معافیت دائم پایان خدمت
    حداقل مدرک تحصیلی
    مهم نیست
");
        }
    }
}
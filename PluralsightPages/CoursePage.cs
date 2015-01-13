﻿using System;
using OpenQA.Selenium;

namespace Pluralsaver.PluralsightPages
{
    public class CoursePage
    {
        static CoursePage()
        {
            Driver.WaitUntilVisible(By.LinkText("Table of contents"));
            if (!IsExpanded)
                ExpandAllLink.Click();
        }

        public static IWebElement ExpandAllLink
        {
            get { return Driver.Instance.FindElement(By.Id("expandAll")); }
        }

        public static bool IsExpanded
        {
            get { return !ExpandAllLink.Displayed; }
        }

        public static void Download(PluralsaverSettings settings)
        {
            var currentCourseTitle = Driver.Instance.FindElement(By.CssSelector("h1.course-title")).Text;

            Console.WriteLine("Downloading course: {0}", currentCourseTitle);
            
            // Create a dir for the current course
            var courseDir = CourseDownloader.CreateDir(settings.Path, currentCourseTitle);
            Console.WriteLine("Into {0}", courseDir);


            var sectionElementList = Driver.Instance.FindElements(By.CssSelector("div.section"));
            Console.WriteLine("Number of sections: {0}", sectionElementList.Count);

            for (var sectionIndex = 0; sectionIndex < sectionElementList.Count; sectionIndex++)
            {
                var sectionElement = sectionElementList[sectionIndex];
                DownloadSection(sectionElement, sectionIndex + 1, courseDir);
            }
        }

        private static void DownloadSection(IWebElement sectionElement, int sectionIndex, string courseDir)
        {
            var sectionTitle = sectionElement.FindElement(By.CssSelector("p.title a")).Text;

            // Create a dir for the current section
            var sectionTitleWithIndex = String.Format("{0}. {1}", sectionIndex, sectionTitle);
            var sectionDir = CourseDownloader.CreateDir(courseDir, sectionTitleWithIndex);
            Console.WriteLine("    Downloading section {0}: {1}", sectionTitleWithIndex, sectionDir);
        }
    }
}

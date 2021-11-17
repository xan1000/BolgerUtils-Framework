using System.IO;
using BolgerUtils.Framework.Razor;
using Tests.BolgerUtils.Framework.Razor.Models;
using Xunit;

namespace Tests.BolgerUtils.Framework.Razor
{
    public class UtilsTests
    {
        private const string TestPath = "Templates/_Test.cshtml";
        private FileInfo TestFileInfo { get; } = new FileInfo(TestPath);

        [Fact]
        public void Test_Parse()
        {
            var expected =
                @"<!DOCTYPE html>
                <html>
                    <head>
                        <title>Test</title>
                    </head>
                    <body>
                        <div>
                            <header>
                                <h1>Test</h1>
                            </header>
                            
                            <main>
                            </main>
                            
                            <footer>
                                &copy; Copyright 2019, Matthew Bolger
                            </footer>
                        </div>
                    </body>
                </html>";
            var testModel = new TestModel();
            Test_Parse_Implementation(expected, testModel);

            expected =
                @"<!DOCTYPE html>
                <html>
                    <head>
                        <title>Test</title>
                    </head>
                    <body>
                        <div>
                            <header>
                                <h1>Test</h1>
                            </header>
                            
                            <main>
                                <ul>
                                    <li></li>
                                </ul>
                            </main>
                            
                            <footer>
                                &copy; Copyright 2019, Matthew Bolger
                            </footer>
                        </div>
                    </body>
                </html>";
            testModel = new TestModel(1);
            Test_Parse_Implementation(expected, testModel);

            expected =
                @"<!DOCTYPE html>
                <html>
                    <head>
                        <title>Test</title>
                    </head>
                    <body>
                        <div>
                            <header>
                                <h1>Test</h1>
                            </header>
                            
                            <main>
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                </ul>
                            </main>
                            
                            <footer>
                                &copy; Copyright 2019, Matthew Bolger
                            </footer>
                        </div>
                    </body>
                </html>";
            testModel = new TestModel(3);
            Test_Parse_Implementation(expected, testModel);

            expected =
                @"<!DOCTYPE html>
                <html>
                    <head>
                        <title>Test</title>
                    </head>
                    <body>
                        <div>
                            <header>
                                <h1>Test</h1>
                            </header>
                            
                            <main>
                                <ul>
                                    <li>Hello World</li>
                                    <li>Hello World</li>
                                    <li>Hello World</li>
                                    <li>Hello World</li>
                                    <li>Hello World</li>
                                </ul>
                            </main>
                            
                            <footer>
                                &copy; Copyright 2019, Matthew Bolger
                            </footer>
                        </div>
                    </body>
                </html>";
            testModel = new TestModel(5, "Hello World");
            Test_Parse_Implementation(expected, testModel);

            expected =
                @"<!DOCTYPE html>
                <html>
                    <head>
                        <title>Test</title>
                    </head>
                    <body>
                        <div>
                            <header>
                                <h1>Test</h1>
                            </header>
                            
                            <main>
                                <ul>
                                    <li>&lt;strong&gt;Hello &amp;nbsp; World&lt;strong&gt;</li>
                                    <li>&lt;strong&gt;Hello &amp;nbsp; World&lt;strong&gt;</li>
                                </ul>
                            </main>
                            
                            <footer>
                                &copy; Copyright 2019, Matthew Bolger
                            </footer>
                        </div>
                    </body>
                </html>";
            testModel = new TestModel(2, "<strong>Hello &nbsp; World<strong>");
            Test_Parse_Implementation(expected, testModel);

            expected =
                @"<!DOCTYPE html>
                <html>
                    <head>
                        <title>Test</title>
                    </head>
                    <body>
                        <div>
                            <header>
                                <h1>Test</h1>
                            </header>
                            
                            <main>
                                <ul>
                                    <li><strong>Hello &nbsp; World<strong></li>
                                    <li><strong>Hello &nbsp; World<strong></li>
                                </ul>
                            </main>
                            
                            <footer>
                                &copy; Copyright 2019, Matthew Bolger
                            </footer>
                        </div>
                    </body>
                </html>";
            testModel = new TestModel(2, "<strong>Hello &nbsp; World<strong>", true);
            Test_Parse_Implementation(expected, testModel);
        }

        private void Test_Parse_Implementation(string expected, TestModel model)
        {
            expected = RemoveRedundantWhitespace(expected);
            Assert.Equal(expected, RemoveRedundantWhitespace(Utils.Parse(TestPath, model)));
            Assert.Equal(expected, RemoveRedundantWhitespace(Utils.Parse(TestFileInfo, model)));
        }

        // https://stackoverflow.com/a/37592018/9798310
        private static string RemoveRedundantWhitespace(string item)
        {
            var length = item.Length;
            var array = item.ToCharArray();
            var arrayIndex = 0;
            var skip = false;
            for(var i = 0; i < length; i++)
            {
                var character = array[i];
                switch(character)
                {
                    case '\r':
                        // Remove all \r.
                        continue;
                    case ' ':
                    case '\t':
                    case '\n':
                        if(skip)
                            continue;
                        array[arrayIndex++] = character;
                        skip = true;
                        break;
                    default:
                        array[arrayIndex++] = character;
                        skip = false;
                        break;
                }
            }

            return new string(array, 0, arrayIndex);
        }
    }
}

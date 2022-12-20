using Microsoft.Playwright;

namespace PlaywrightSpecFlowPOM.Hooks
{
    [Binding]
    public class Hooks
    {
        public IPage User { get; private set; } = null!; //-> We'll call this property in the tests

        [BeforeScenario] // -> Notice how we're doing these steps before each scenario
        public async Task RegisterSingleInstancePractitioner()
        {
            //Initialise Playwright
            var playwright = await Playwright.CreateAsync();
            //Initialise a browser - 'Chromium' can be changed to 'Firefox' or 'Webkit'
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // -> Use this option to be able to see your test running
            });
            //Setup a browser context
            var context1 = await browser.NewContextAsync();

            //Initialise a page on the browser context.
            User = await context1.NewPageAsync(); 
        }
    }
}
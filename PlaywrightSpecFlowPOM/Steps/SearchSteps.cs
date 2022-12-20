using Microsoft.Playwright;
using PlaywrightSpecFlowPOM.Pages;

namespace PlaywrightSpecFlowPOM.Steps;

[Binding]
public class SearchSteps
{
    private readonly IPage _user;
    private readonly DuckDuckGoHomePage _duckDuckGoHomePage;

    public SearchSteps(Hooks.Hooks hooks, DuckDuckGoHomePage duckDuckGoHomePage)
    {
        _user = hooks.User;
        _duckDuckGoHomePage = duckDuckGoHomePage;
    }
    
    [Given(@"the user is on the DuckDuckGo homepage")]
    public async Task GivenTheUserIsOnTheDuckDuckGoHomepage()
    { 
        //Go to the DuckDuckGo homepage
        await _user.GotoAsync("https://duckduckgo.com/");
        
        //Assert the page
        await _duckDuckGoHomePage.AssertPageContent();
    }

    [When(@"the user searches for '(.*)'")]
    public async Task WhenTheUserSearchesFor(string searchTerm)
    {
        //Type the search term and press enter
        await _duckDuckGoHomePage.SearchAndEnter(searchTerm);
    }
}
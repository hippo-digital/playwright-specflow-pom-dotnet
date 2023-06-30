using Microsoft.Playwright;

namespace PlaywrightSpecFlowPOM.Pages;

public class DuckDuckGoHomePage
{
    private readonly IPage _user;

    public DuckDuckGoHomePage(Hooks.Hooks hooks)
    {
        _user = hooks.User;
    }

    private ILocator SearchInput => _user.Locator("input[id='searchbox_input']");
    private ILocator SearchButton => _user.Locator("button[type='submit']");
    
    public async Task AssertPageContent()
    {
        await Assertions.Expect(_user).ToHaveURLAsync("https://duckduckgo.com/");
        await Assertions.Expect(SearchInput).ToBeVisibleAsync();
        await Assertions.Expect(SearchButton).ToBeVisibleAsync();
    }

    public async Task SearchAndEnter(string searchTerm)
    {
        await SearchInput.TypeAsync(searchTerm);
        await SearchButton.ClickAsync();
    }
}
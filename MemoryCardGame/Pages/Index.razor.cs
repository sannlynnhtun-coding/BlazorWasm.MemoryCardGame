using MemoryCardGame.Models;

namespace MemoryCardGame.Pages;

public partial class Index
{
    private bool startGame = false;
    private List<MemoryCardGameModel> cards = new List<MemoryCardGameModel>();
    private int[] intArr = new int[16];
    private int selectedCardCount = 0;
    private int[] selectedCards = new int[2];
    private int moves = 0;
    private bool gameOver = false;
    private bool isClicked = true;

    protected override void OnInitialized()
    {

    }

    async Task Start()
    {
        isClicked = true;
        gameOver = false;
        var tempCards = new List<MemoryCardGameModel>()
        {
            new MemoryCardGameModel() { Value = "bee", Image = "bee.png" },
            new MemoryCardGameModel() { Value = "crocodile", Image = "crocodile.png" },
            new MemoryCardGameModel() { Value = "macaw", Image = "macaw.png" },
            new MemoryCardGameModel() { Value = "gorilla", Image = "gorilla.png" },
            new MemoryCardGameModel() { Value = "tiger", Image = "tiger.png" },
            new MemoryCardGameModel() { Value = "monkey", Image = "monkey.png" },
            new MemoryCardGameModel() { Value = "chameleon", Image = "chameleon.png" },
            new MemoryCardGameModel() { Value = "piranha", Image = "piranha.png" },
            new MemoryCardGameModel() { Value = "anaconda", Image = "anaconda.png" },
            new MemoryCardGameModel() { Value = "sloth", Image = "sloth.png" },
            new MemoryCardGameModel() { Value = "cockatoo", Image = "cockatoo.png" },
            new MemoryCardGameModel() { Value = "toucan", Image = "toucan.png" },
        };
        moves = 0;
        int Id = 0;
        int CardCount = 8;
        cards = new List<MemoryCardGameModel>();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < CardCount; j++)
            {
                var card = tempCards[j];
                cards.Add(new MemoryCardGameModel
                {
                    Image = card.Image,
                    Id = ++Id,
                    Value = card.Value,
                    IsSelected = true
                });
            }
        }
        startGame = true;
        Random random = new Random();
        for (int i = 1; i <= 16; i++)
        {
            intArr[i - 1] = i;
        }
        intArr = intArr.OrderBy(x => random.Next()).ToArray();
        await Task.Delay(TimeSpan.FromSeconds(5));
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].IsSelected = false;
        }
        isClicked = false;
        StateHasChanged();
    }

    void Stop()
    {
        if (isClicked) return;
        startGame = false;
    }

    async void CardClick(MemoryCardGameModel card)
    {
        if (isClicked) return;
        if (card.IsMatched || card.IsSelected)
        {
            return;
        }
        isClicked = true;
        card.IsSelected = true;
        selectedCards[selectedCardCount++] = card.Id;

        if (selectedCardCount == 2)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            moves++;

            var card1 = cards.First(c => c.Id == selectedCards[0]);
            var card2 = cards.First(c => c.Id == selectedCards[1]);
            if (card1.Value != card2.Value)
            {
                cards.First(c => c.Id == selectedCards[0]).IsSelected = false;
                cards.First(c => c.Id == selectedCards[1]).IsSelected = false;
            }
            else
            {
                cards.First(c => c.Id == selectedCards[0]).IsMatched = true;
                cards.First(c => c.Id == selectedCards[1]).IsMatched = true;
                gameOver = cards.All(c => c.IsMatched);
            }
            selectedCards = new int[2];
            selectedCardCount = 0;
        }
        isClicked = false;
        StateHasChanged();
    }
}
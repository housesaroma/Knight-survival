public static class BattleScene
{
    private Player player;
    private Monster monster;
    private SpriteBatch spriteBatch;
    private SpriteFont font;
    private Random random = new Random();

    public static BattleScene(Player player, Monster monster, SpriteBatch spriteBatch, SpriteFont font)
    {
        this.player = player;
        this.monster = monster;
        this.spriteBatch = spriteBatch;
        this.font = font;
    }

    public void LoadContent(ContentManager content)
    {
        // Загрузка необходимых ресурсов
    }

    public void Update(GameTime gameTime)
    {
        // Обновление логики битвы
        RollDice();
    }

    public void Draw(GameTime gameTime)
    {
        // Отрисовка сцены битвы
        spriteBatch.Begin();
        DrawDiceResults();
        spriteBatch.End();
    }

    private void RollDice()
    {
        int playerRoll = random.Next(1, 7); // Бросок кубика от 1 до 6
        int monsterRoll = random.Next(1, 7);
        DisplayRollResult(player, playerRoll);
        DisplayRollResult(monster, monsterRoll);
        DetermineAttacker(playerRoll, monsterRoll);
    }

    private void DrawDiceResults()
    {
        // Отрисовка результатов броска
    }
}
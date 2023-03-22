namespace PlatePal.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Recipe CreateRecipe(Recipe recipeData)
        {
            string sql = @"
        INSERT INTO
    recipes (
        title,
        instructions,
        img,
        category,
        creatorId
    )VALUES (
       @title,
        @instructions,
        @img,
        @category,
        @creatorId
    );
    SELECT LAST_INSERT_ID();
        ";
            int id = _db.ExecuteScalar<int>(sql, recipeData);
            recipeData.Id = id;
            return recipeData;
        }

        internal List<Recipe> GetAll()
        {
            string sql = @"
            SELECT 
            rec.*, acct.*
            FROM recipes rec
            JOIN accounts acct ON rec.creatorId = acct.id;
            ";
            List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
            {
                recipe.Creator = account;
                return recipe;
            }).ToList();
            return recipes;
        }

        internal Recipe GetById(int id)
        {
            string sql = @"
            SELECT 
            rec.*, acct.*
            FROM recipes rec
            JOIN accounts acct ON rec.creatorId = acct.id
            WHERE rec.id = @id;
            ";
            Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
            {
                recipe.Creator = account;
                return recipe;
            }, new { id }).FirstOrDefault();
            return recipe;
        }
    }
}
namespace PlatePal.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Recipe> GetAll()
        {
            string sql = @"
            SELECT 
            rec.*, acct.*
            FROM recipes rec
            JOIN accounts acct ON rec.creatorId = acct.id;
            ";
            List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
            {
                recipe.Creator = profile;
                return recipe;
            }).ToList();
            return recipes;
        }
    }
}
namespace PlatePal.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal List<Recipe> GetAll()
        {
            List<Recipe> recipes = _repo.GetAll();
            return recipes;
        }
    }
}
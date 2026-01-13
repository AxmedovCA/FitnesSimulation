namespace FitnessSimulation.ViewModels.TrainerViewModel
{
    public class TrainerGetVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }=string.Empty;
        public int Experience { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public string CategoryName { get; set; }=string.Empty;
    }
}

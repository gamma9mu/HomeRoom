namespace HomeRoom
{
    public class StudentInformation
    {
        public int visualPercent { get; private set; }
        public int auralPercent { get; private set; }
        public int tactilePercent { get; private set; }

        public StudentInformation(int vp, int ap, int tp)
        {
            visualPercent = vp;
            auralPercent = ap;
            tactilePercent = tp;
        }
    }
}

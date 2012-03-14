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

    public class Request
    {
        public int connectionSpeed { get; private set; }
        public string topic { get; private set; }
        public StudentInformation studentInformation { get; private set; }

        public Request(StudentInformation student, string topic, int connectionSpeed)
        {
            this.studentInformation = student;
            this.topic = topic;
            this.connectionSpeed = connectionSpeed;
        }
    }
}

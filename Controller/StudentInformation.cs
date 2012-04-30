namespace HomeRoom
{
    /// <summary>
    /// Represents the learning style of the requesting user.
    /// </summary>
    public class StudentInformation
    {
        /// <summary>
        /// The weighting towards visual learning of the user's learning style.
        /// </summary>
        public int visualPercent { get; private set; }
        
        /// <summary>
        /// The weighting towards aural learning of the user's learning style.
        /// </summary>
        public int auralPercent { get; private set; }
        
        /// <summary>
        /// The weighting towards tactile learning of the user's learning style.
        /// </summary>
        public int tactilePercent { get; private set; }

        /// <summary>
        /// Convenience constructor.
        /// </summary>
        /// <param name="vp">The value for <code>visualPercent</code>.</param>
        /// <param name="ap">The value for <code>auralPercent</code>.</param>
        /// <param name="tp">The value for <code>tactilePercent</code>.</param>
        public StudentInformation(int vp, int ap, int tp)
        {
            visualPercent = vp;
            auralPercent = ap;
            tactilePercent = tp;
        }
    }

    /// <summary>
    /// Represents a user's request passed from the front-end.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// The user's connection speed in bytes per second.
        /// </summary>
        public int connectionSpeed { get; private set; }

        /// <summary>
        /// The topic the user is interested in.
        /// </summary>
        public string topic { get; private set; }

        /// <summary>
        /// The student's learning style weights.
        /// </summary>
        public StudentInformation studentInformation { get; private set; }

        /// <summary>
        /// Convenience constructor.
        /// </summary>
        /// <param name="student">The value for <code>studentInformation</code>
        /// .</param>
        /// <param name="topic">The value for <code>topic</code>.</param>
        /// <param name="connectionSpeed">The value for
        /// <code>connectionSpeed</code>.</param>
        public Request(StudentInformation student, string topic, int connectionSpeed)
        {
            this.studentInformation = student;
            this.topic = topic;
            this.connectionSpeed = connectionSpeed;
        }
    }
}

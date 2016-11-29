namespace WTBoxPannel
{
    public class ChartModelCollection
    {
        public ChartModelCollection()
        {

        }

        private double xmax;
        private double xmin;
        private double ymax;
        private double ymin;
        private double xstep;
        private double ystep;

        public double Xmin
        {
            get { return xmin; }
            set { xmin = value; }
        }

        public double Xmax
        {
            get { return xmax; }
            set { xmax = value; }
        }

        public double Ymin
        {
            get { return ymin; }
            set { ymin = value; }
        }

        public double Ymax
        {
            get { return ymax; }
            set { ymax = value; }
        }
        public double Xstep
        {
            get { return xstep; }
            set { xstep = value; }
        }

        public double Ystep
        {
            get { return ystep; }
            set { ystep = value; }
        }
    }
}

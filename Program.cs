using System.Security.Cryptography.X509Certificates;

namespace OriGyak
{
    public interface ITest
    {
        double Felszin();
        double Terfogat();
    }

    public interface IKezel
    {
        /// <summary>
        /// A megadott mennyiséget betölti a testbe.
        /// </summary>
        /// <param name="mennyit">A töltés mennyisége.</param>
        void Tolt(double mennyit);
        /// <summary>
        /// Kiüríti a testet, így a töltöttsége 0% lesz!
        /// </summary>
        void Kiurit();
        //Megadja, hogy a test térfogata mekkora %-ban van kitöltve.
        double JelenlegiToltottsegSzazalekban { get; }


    }

    public class Gomb : ITest, IKezel
    {
        double sugar;
        double toltet;
        public double JelenlegiToltottsegSzazalekban => toltet / Terfogat() * 100;
        public Gomb(double sugar)
        {
            this.sugar = sugar;
        }

        public void Tolt(double mennyit) 
        {
            if (toltet + mennyit > Terfogat())
                throw new OverflowException();
            toltet += mennyit;
        }
        public void Kiurit() => toltet = 0;
        public double Felszin() => 4*Math.PI*Math.Pow(sugar,2); 
        public double Terfogat() => 4*Math.PI/3*Math.Pow(sugar,3);
        public override string? ToString() => $"sugár: {sugar}; töltet: {toltet}; töltöttség százalékban: {JelenlegiToltottsegSzazalekban}%; Térfogat: {Terfogat():f2}; Felszín: {Felszin():f2}";
    }

    public class Teglatest : ITest, IKezel
    {
        double aEl,bEl,cEl;
        double toltet;
        public double JelenlegiToltottsegSzazalekban => toltet / Terfogat() * 100;
        public Teglatest(double aEl,double bEl,double cEl)
        {
            this.aEl = aEl;
            this.bEl = bEl;
            this.cEl = cEl;
        }
        public double Felszin() => 2*aEl+bEl+2*aEl*cEl+2*bEl*cEl;
        public double Terfogat() => aEl * bEl * cEl;
        public void Tolt(double mennyit)
        {
            if (toltet + mennyit > Terfogat())
                throw new OverflowException();
            toltet += mennyit;
        }
        public void Kiurit() => toltet = 0;
        public override string? ToString() => $"aOldal: {aEl}; bOlal: {bEl}; cOldal: {cEl}; töltet: {toltet}; töltöttség százalékban: {JelenlegiToltottsegSzazalekban}%; Térfogat: {Terfogat():f2}; Felszín: {Felszin():f2}";
    }

    public class Henger : ITest, IKezel
    {
        double sugar;
        double magassag;
        double toltet;
        public double JelenlegiToltottsegSzazalekban => toltet / Terfogat() * 100;
        public Henger(double sugar,double magassag)
        {
            this.sugar = sugar;
            this.magassag = magassag;
        }
        public double Terfogat() => Math.Pow(sugar, 2) * Math.PI * magassag;
        public double Felszin() => 2 * Math.Pow(sugar, 2) * Math.PI + 2 * sugar * Math.PI * magassag;
        public void Tolt(double mennyit)
        {
            if (toltet + mennyit > Terfogat())
                throw new OverflowException();
            toltet += mennyit;
        }
        public void Kiurit() => toltet = 0;

        public override string? ToString() => $"sugár: {sugar}; magasság: {magassag}; töltet: {toltet}; töltöttség százalékban: {JelenlegiToltottsegSzazalekban}%; Térfogat: {Terfogat():f2}; Felszín: {Felszin():f2}";

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List <ITest> lista = new List <ITest> ();
            lista.Add(new Gomb(10));
            lista.Add(new Henger(10, 20));
            lista.Add(new Teglatest(5,10,20));
            lista.ForEach(x => Console.WriteLine(x.ToString()));
        }
    }
}
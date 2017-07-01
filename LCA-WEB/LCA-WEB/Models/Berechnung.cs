using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCA_WEB.Models
{
    public class Berechnung
    {
        /* Umweltindikatoren: 
         * KRA = kumulierter Rohstoffaufwand
         * KEA = kumulierter Energieaufwand
         * CO2-Äquvalente in t
         
      Grundlegende Berechnung: 
         
       Herstellung:
            --- je Umweltindikator (KRA, KRE, CO2-Äqu.) ---
         Menge je Rohstoff in t = Produkt.Stückzahl * Produkt.Rohstoff.Menge in t
         UmweltindikatorVorstufe = Wert aus DB; Umweltauswirkung eines Rohstoffs bei Herstellung
         Umweltauswirkungen je Rohstoff = Menge je Rohstoff in t * Rohstoff.UmweltindikatorVorstufe 
         UmweltindikatorHerstellungGesamt  = Summe Umweltauswirkungen je Rohstoff 
         
            --- Zusätzlich ---
         Materialkosten = Wert aus DB
         Materialkosten je Rohstoff = Menge je Rohstoff in t * Rohstoff.Materialkosten
         MaterialkostenGesamt = Summe Materialkosten je Rohstoff
              
         -Kosten Produktionsmittel = ? Nur Wert oder Berechnung? 
         -Stückzahl wird vom Nutzer eingegeben

       Nutzung: 
            --- einmal für ein Produkt verfügbar --- 
         Energieverbrauch über ND in GJ = Produkt.ND * Produkt.Enerverbrauch/a in GJ
         CO2-Ausstoß in t über ND = Produkt.ND * Produkt.CO2-Ausstoß in t/a
         Kosten pro Jahr in € = Produkt.Energiekosten/GJ in Euro * Produkt.Enerverbrauch/a in GJ
         Kosten über die gesamte ND in € = Produkt.ND * Kosten pro Jahr in € 	

            --- mehrmals für mehrere Produktionsmittel verfügbar (aus denen das Produkt besteht; bisher 3x) --- 
         Menge je Rohstoff in t = wie oben
         Menge ND in t = Menge je Rohstoff in t * (Produkt.ND/Produkt.Instandhaltungsrhythmus in a)
         Umweltindikator je Rohstoff = Rohstoff.Umweltindikator.Nutzung * Menge ND in t
                [[vorige Zeile konkret:
                KRA je Rohstoff = Rohstoff.RohstoffaufwandNutzung * Menge ND in t
                KEA je Rohstoff = Rohstoff.EnergieaufwandNutzung * Menge ND in t
                CO2-Äquiv. je Rohstoff = Rohstoff.CO2ÄquvalenteNutzung * Menge ND in t]]
         UmweltindikatorNutzung = Summe Umweltindikator je Rohstoff (je Indikator)

         Kosten in Euro = Produkt.Instandhaltungskosten (scheinbar keine Berechnung...?)
         
            ---- Gesamt ---
         UmweltindikatorNutzungGesamt = Summe UmweltindikatorNutzung der Produktionsmittel
         

         -Rohstoffaufwand, Energieaufwand und CO2Äquvalente jetzt vermutlich nur Einzelwerte, könnten (später) aber auch Summen sein
         -bisher wohl nur auf Grundlage von Wasserverbrauch berechnet
         -hier vermutlich noch Änderung notwendig: ein Produkt kann aus meheren Produktionsmitteln/Bauteilen bestehen; 
          deren summierte Umweltauswirkungen ergeben die Gesamtsumme je Umweltindkator


       End of Life:
            --- Umweltauswirkungen bei Herstellung ---

         ----> wie bei Herstellung (Absatz "je Umweltindikator")

            --- Umweltauswirkungen bei Recycling ---
         Recyclingmenge je Rohstoff in t = Menge in t * Rohstoffanteil recyclingfähig
         UmweltindikatorRecycling je Rohstoff = Recyclingmenge je Rohstoff in t * Rohstoff.Umweltindikator.Recycling
         UmweltindikatorRecyclingGesamt = Summe UmweltindikatorRecycling je Rohstoff

            --- Entlastung je Umweltindikator ---
         absolute Umweltentlastung = UmweltindikatorHerstellungGesamt - UmweltindikatorRecyclingGesamt
         relative Umweltentlastung = absolute Umweltentlastung / UmweltindikatorHerstellungGesamt

            --- Zusätzlich ---
         Entsorgungskosten je Rohstoff = Wert aus DB
         Recyclingerlöse je Rohstoff = Wert aus DB
         EntsorgungskostenGesamt = Summe Entsorgungskosten je Rohstoff
         RecyclingerlöseGesamt = Recyclingerlöse je Rohstoff
         */
    }
}
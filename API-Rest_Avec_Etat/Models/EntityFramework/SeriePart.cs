namespace API_Rest_Avec_Etat.Models.EntityFramework
{
    public partial class Serie
    {
        public override bool Equals(object? obj)
        {
            return obj is Serie serie &&
                   this.Serieid == serie.Serieid &&
                   this.Titre == serie.Titre &&
                   this.Resume == serie.Resume &&
                   this.Nbsaisons == serie.Nbsaisons &&
                   this.Nbepisodes == serie.Nbepisodes &&
                   this.Anneecreation == serie.Anneecreation &&
                   this.Network == serie.Network;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Serieid, this.Titre, this.Resume, this.Nbsaisons, this.Nbepisodes, this.Anneecreation, this.Network);
        }

        public Serie(int serieid, string titre, string? resume, int? nbsaisons, int? nbepisodes, int? anneecreation, string? network)
        {
            this.Serieid = serieid;
            this.Titre = titre;
            this.Resume = resume;
            this.Nbsaisons = nbsaisons;
            this.Nbepisodes = nbepisodes;
            this.Anneecreation = anneecreation;
            this.Network = network;
        }
    }
}

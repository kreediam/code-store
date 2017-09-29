    public class CreateTaxa
    {
        TaxonomyManager taxMan = TaxonomyManager.GetManager();
        HierarchicalTaxonomy taxonomy;

        public void DoIt()
        {
            // throw exception if not found
            taxonomy = taxMan.GetTaxonomies<HierarchicalTaxonomy>().First(s => s.Name == "Categories");

            this.CreateOne("ACV", "Commercial");
            this.CreateOne("BO", "Commercial");
            this.CreateOne("CF", "Commercial");
            this.CreateOne("CPP", "Commercial");
            this.CreateOne("GL", "Commercial");
            this.CreateOne("GR", "Commercial");
            this.CreateOne("IMC", "Commercial");
            this.CreateOne("ULC", "Commercial");
            this.CreateOne("WC", "Commercial");

            this.CreateOne("FL", "Farm");
            this.CreateOne("FM", "Farm");
            this.CreateOne("FO", "Farm");
            this.CreateOne("ULP", "Farm");

            this.CreateOne("APV", "Personal");
            this.CreateOne("BT", "Personal");
            this.CreateOne("DF", "Personal");
            this.CreateOne("HO", "Personal");
            this.CreateOne("IMP", "Personal");
            this.CreateOne("YAC", "Personal");
            this.CreateOne("YAP", "Personal");

            taxMan.SaveChanges();
        }

        private void CreateOne(string title, string parentTitle)
        {
            var parent = taxMan.GetTaxa<HierarchicalTaxon>().First(t => t.Title == parentTitle);

            var taxon = taxMan.CreateTaxon<HierarchicalTaxon>();
            taxon.Title = title;
            taxon.Name = title;
            taxon.UrlName = new Lstring(Regex.Replace(title, @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-").ToLower());
            taxon.Description = title;
            taxon.Taxonomy = taxonomy;
            taxon.Parent = parent;

            taxonomy.Taxa.Add(taxon);
        }
    }

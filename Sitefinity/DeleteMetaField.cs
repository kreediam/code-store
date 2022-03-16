    public void DeleteMetaField(object sender, EventArgs e)
    {
        // select * from sf_meta_types order by last_modified
        var metaTypeManager = Telerik.Sitefinity.Data.Metadata.MetadataManager.GetManager();
        var metaType = metaTypeManager.GetMetaTypes().FirstOrDefault(i => i.Id == Guid.Parse("43721AF5-3C1D-45F0-BD17-236B53CF4D96")); //the id of the meta type
        var field = metaType.Fields.FirstOrDefault(f => f.FieldName == "Title");
        metaTypeManager.Delete(field);
        metaTypeManager.SaveChanges();
    }

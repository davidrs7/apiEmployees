
namespace Api.Queries
{
    public class StepQueries
    {
        public string Steps { get; } = "SELECT Id, Available, Name, Description FROM step";
        public string StepById { get; } = "SELECT Id, Available, Name, Description FROM step WHERE Id = @Id";
        public string Add { get; } = "INSERT INTO step (Available, Name, Description) VALUES (@Available, @Name, @Description); SELECT LAST_INSERT_ID();;";
        public string Edit { get; } = "UPDATE step SET Available = @Available, Name = @Name, Description = @Description WHERE Id = @Id";
        public string Fields { get; } = "SELECT Id, Available, Name, FieldType, IsRequired, Config FROM step_field";
        public string FieldById { get; } = "SELECT Id, Available, Name, FieldType, IsRequired, Config FROM step_field WHERE Id = @Id";
        public string FieldsByStep { get; } = "SELECT SF.Id, SFR.StepId, SF.Available, SF.Name, SF.FieldType, SF.IsRequired, SF.Config, SFR.Active, SFR.Weight FROM step_field SF INNER JOIN step_field_rel SFR ON SF.Id = SFR.StepFieldId WHERE SFR.StepId = @StepId ORDER BY SFR.Weight";
        public string FieldsByStepRel { get; } = "SELECT SF.Id, SFR.StepId, PVR.VacantId, PVR.PostulateId, SF.Available, SF.Name, SF.FieldType, SF.IsRequired, SF.Config, SFR.Active, SFR.Weight, PVSFR.FieldValue FROM step_field SF INNER JOIN step_field_rel SFR ON SF.Id = SFR.StepFieldId LEFT JOIN vacant_step_rel VSR ON (VSR.StepId = SFR.StepId AND VSR.VacantId = @VacantId) LEFT JOIN postulate_vacant_rel PVR ON (VSR.VacantId = PVR.VacantId AND PVR.PostulateId = @PostulateId) LEFT JOIN postulate_vacant_step_field_rel PVSFR ON (SFR.Id = PVSFR.StepFieldRelId AND PVR.Id = PVSFR.PostulateVacantRelId) WHERE SFR.StepId = @StepId ORDER BY SFR.Weight";
        public string FieldAdd { get; } = "INSERT INTO step_field (Available, Name, FieldType, IsRequired, Config) VALUES (@Available, @Name, @FieldType, @IsRequired, @Config)";
        public string FieldEdit { get; } = "UPDATE step_field SET Available = @Available, Name = @Name, FieldType = @FieldType, IsRequired = @IsRequired, Config = @Config WHERE Id = @Id";
        public string FieldRelAdd { get; } = "INSERT INTO step_field_rel (StepId, StepFieldId, Active, Weight) VALUES (@StepId, @StepFieldId, @Active, @Weight)";
        public string FieldRelEdit { get; } = "UPDATE step_field_rel SET Active = @Active, Weight = @Weight WHERE StepId = @StepId AND StepFieldId = @StepFieldId";
        public string FieldRelValueAdd { get; } = "INSERT INTO postulate_vacant_step_field_rel (PostulateVacantRelId, StepFieldRelId, FieldValue) VALUES (@PostulateVacantRelId, @StepFieldRelId, @FieldValue)";
        public string FieldRelValueEdit { get; } = "UPDATE postulate_vacant_step_field_rel SET FieldValue = @FieldValue WHERE PostulateVacantRelId = @PostulateVacantRelId AND StepFieldRelId = @StepFieldRelId";
        public string StepPostRelFindId { get; } = "SELECT Id FROM postulate_vacant_rel WHERE VacantId = @VacantId AND PostulateId = @PostulateId";
        public string StepFieldRelFindId { get; } = "SELECT Id FROM step_field_rel WHERE StepId = @StepId AND StepFieldId = @StepFieldId";
    }
}
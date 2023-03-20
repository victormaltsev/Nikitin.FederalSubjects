namespace Nikitin.FederalSubjects.Application.Identities;

public readonly struct FederalSubjectTypeId
{
    private readonly short _id;

    public FederalSubjectTypeId(short id)
    {
        _id = id;
    }

    public override string ToString() =>
        _id.ToString();

    public static implicit operator short(FederalSubjectTypeId federalSubjectTypeId) => federalSubjectTypeId._id;
    public static explicit operator FederalSubjectTypeId(short id) => new(id);
}

namespace Nikitin.FederalSubjects.Application.Identities;

public readonly struct FederalSubjectId
{
    private readonly short _id;

    public FederalSubjectId(short id)
    {
        _id = id;
    }

    public override string ToString() =>
        _id.ToString();

    public static implicit operator short(FederalSubjectId federalSubjectId) => federalSubjectId._id;
    public static explicit operator FederalSubjectId(short id) => new(id);
}

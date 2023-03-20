namespace Nikitin.FederalSubjects.Application.Identities;

public readonly struct FederalDistrictId
{
    private readonly short _id;

    public FederalDistrictId(short id)
    {
        _id = id;
    }

    public override string ToString() =>
        _id.ToString();

    public static implicit operator short(FederalDistrictId federalDistrictId) => federalDistrictId._id;
    public static explicit operator FederalDistrictId(short id) => new(id);
}

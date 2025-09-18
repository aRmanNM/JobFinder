namespace JobFinder.Server.Models;

public class JobvisionListResponse : BaseWebHelperResponse
{
    public JobvisionListData? data { get; set; }
}

public class JobvisionListData
{
    public List<JobvisionJobPost> jobPosts { get; set; } = new List<JobvisionJobPost>();
}

public class JobvisionJobPost
{
    public string? title { get; set; }
    public string? id { get; set; }
    public JobvisionCompany? company { get; set; }
    public JobvisionActivationTime? activationTime { get; set; }
}

public class JobvisionActivationTime
{
    public string? beautifyFa { get; set; }
}

public class JobvisionCompany
{
    public string? logoFileId { get; set; }
    public string? logoUrl { get; set; }
    public string? nameFa { get; set; }
    public string? MyProperty { get; set; }
    public JobvisionCompanyLocation? location { get; set; }
}

public class JobvisionCompanyLocation
{
    public JobvisionCompanyLocationProvince? province { get; set; }
}

public class JobvisionCompanyLocationProvince
{
    public string? titleFa { get; set; }
}
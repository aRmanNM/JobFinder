export interface JobAd {
    title: string | null;
    url: string | null;
    description: string | null;
    company: string | null;
    logoUrl: string | null;
    location: string | null;
    contract: string[];
    experience: string | null;
    dateIdentifier: string | null;
    abilities: string[];
}
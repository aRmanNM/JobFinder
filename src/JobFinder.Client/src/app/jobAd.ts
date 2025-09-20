export interface JobAd {
  id: string;
  serviceName: string;
  title: string;
  url: string | null;
  description: string | null;
  company: string | null;
  logoUrl: string | null;
  location: string | null;
  contract: string[];
  experience: string | null;
  dateIdentifier: string | null;
  abilities: string[];

  bookmarked: boolean;
}

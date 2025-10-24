import { JobAd } from "./job-ad";

export interface SourceAds {
    serviceName: string;
    ads: JobAd[];
    pageNumber: number;
}
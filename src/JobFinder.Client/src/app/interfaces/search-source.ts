import { JobAd } from "./job-ad";

export interface SearchSource {
    title: string;
    titleFa: string;
    isEnabled: boolean;
    ads: JobAd[];
    pageNumber: number;
}
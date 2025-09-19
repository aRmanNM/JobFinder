import { JobAd } from "./jobAd";

export interface SearchSource {
    title: string;
    titleFa: string;
    isEnabled: boolean;
    ads: JobAd[];
    pageNumber: number;
}
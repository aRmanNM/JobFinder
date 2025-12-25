import { JobAd } from "./job-ad";

export interface Bookmark {
    id: number;
    note: string | null;
    content: JobAd;
    userId: string;
    createdAt: string | Date | null;
    lastEditAt: string | null;
}
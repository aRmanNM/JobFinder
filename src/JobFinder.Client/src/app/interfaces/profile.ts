export interface ProfileModel {
    username: string | null;
    pictureUid: string | null;
    joinedAt: string;
    searchCount: number;
    recentQueries: RecentQuery[];
    tags: string[];
}

export interface RecentQuery {
    id: number;
    userId: string;
    query: string;
    createdAt: string;
    count: number;
}
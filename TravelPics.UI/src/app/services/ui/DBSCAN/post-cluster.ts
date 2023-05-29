import { MapPost } from "../../api/dtos/map-post";

export class PostsCluster{
    public clusterId: number = 0;
    public latitude: number = 0;
    public longitude: number = 0;
    public posts: MapPost[] = [];
    public clusterProfileImageSrc: string = '';

    constructor(clusterId: number){
        this.clusterId = clusterId;
    }
}
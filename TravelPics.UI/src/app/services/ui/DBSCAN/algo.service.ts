import { Injectable } from '@angular/core';
import { DBSCANConfig } from './DBSCANConfig';
import { Location } from '../../api/dtos/location';
import { MapPost } from '../../api/dtos/map-post';
import { PostsCluster } from './post-cluster';
import { ImageService } from '../helpers/image.service';

@Injectable({
  providedIn: 'root'
})
export class AlgoService {

  private readonly ClusterProperties: Map<number, DBSCANConfig> = new Map<number, DBSCANConfig>([
    [3, new DBSCANConfig(10, 1)],
    [4, new DBSCANConfig(5, 1)],
    [5, new DBSCANConfig(2, 1)],
    [6, new DBSCANConfig(1, 1)],
  ]);
  
  constructor(
    public imageHelperService: ImageService
  ) { }

  public getPostsClustered(zoomLevel: number, mapPosts: MapPost[]): PostsCluster[]{
    let clusters: PostsCluster[] = [];
    let algoParameters: DBSCANConfig | undefined = this.ClusterProperties.get(zoomLevel);

    if(!!algoParameters){
      clusters = this.performDBSCAN(mapPosts, algoParameters.epsilon, algoParameters.minPoints);
    }
    if(clusters.length > 0){
      for(let c of clusters){
        c = this.calculateCentralPoint(c);
        let photo = c.posts[c.posts.length - 1].photos[c.posts[c.posts.length - 1].photos.length - 1];
        c.clusterProfileImageSrc = this.imageHelperService.getSanitizedBlobUrlFromBase64(photo.content, photo.fileName).changingThisBreaksApplicationSecurity;
      }
    }

    return clusters;
  }

  public performDBSCAN(mapPosts: MapPost[], epsilon: number, minPoints: number): PostsCluster[]{
    let clusters: PostsCluster[] = [];
    let visited: Set<MapPost> = new Set<MapPost>();

    let clusterId: number = 1;

    epsilon = epsilon * epsilon;

    for(let p of mapPosts){
      if(visited.has(p)) continue;

      visited.add(p);

      let neighbors: MapPost[] = this.getRegion(mapPosts, p, epsilon);

      if(neighbors.length >= minPoints){
        let cluster = new PostsCluster(clusterId);
        cluster.posts.push(p);

        this.expandCluster(mapPosts,p, neighbors, cluster, epsilon, minPoints, visited);
        clusters.push(cluster);

        clusterId++;
      }
    }

    return clusters;
  }

  private expandCluster(mapPosts: MapPost[], mapPost: MapPost, neighbors: MapPost[], cluster: PostsCluster, epsilon: number, minPts: number, visited: Set<MapPost>): void{
    let seeds: MapPost[] = [...neighbors];

    while (seeds.length > 0) {
      let currentP: MapPost | undefined = seeds.shift();
      if(currentP == undefined) continue;

      visited.add(currentP);
  
      const currentNeighbors = this.getRegion(mapPosts, currentP, epsilon);
  
      if (currentNeighbors.length >= minPts) {
        for (const neighbor of currentNeighbors) {
          if (!visited.has(neighbor)) {
            seeds.push(neighbor);
            visited.add(neighbor);
          }
        }
      }
  
      if (!cluster.posts.includes(currentP)) {
        cluster.posts.push(currentP);
      }
    }

  }

  private getRegion(mapPosts: MapPost[], mapPost:MapPost, epsilon: number): MapPost[]{
    let region: MapPost[] = [];
    for(let mp of mapPosts){
      const distSquared: number = this.calculateSquaredDistance(mapPost.location,mp.location);
      if(distSquared <= epsilon) region.push(mp);
    }
    return region;
  }

  private calculateSquaredDistance(loc1: Location, loc2: Location): number{
    let diffLong = loc2.longitude - loc1.longitude;
    let diffLat = loc2.latitude - loc1.latitude;
    return diffLong * diffLong + diffLat * diffLat;
  }

  private calculateCentralPoint(cluster: PostsCluster): PostsCluster {
    let totalLongitude: number = 0.0;
    let totalLatitude: number = 0.0;
    let count: number = cluster.posts.length;

    for (const p of cluster.posts) {
        totalLongitude += p.location.longitude;
        totalLatitude += p.location.latitude;
    }

    let avgLongitude: number = totalLongitude / count;
    let avgLatitude: number = totalLatitude / count;

    cluster.longitude = avgLongitude;
    cluster.latitude = avgLatitude;

    return cluster;
  }

}

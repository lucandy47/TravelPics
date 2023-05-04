import { PostImage } from "src/app/services/api/dtos/post-image";

export class ImageHelper{

    public static async loadImages(files: File[]): Promise<PostImage[]> {
        const images: PostImage[] = [];
        for (let i = 0; i < files.length; i++) {
          const imageDataUrl = await this.readFileAsDataURL(files[i]);
          images.push({
            itemImageSrc: imageDataUrl,
            thumbnailImageSrc: imageDataUrl
          });
        }
        return images;
      }
      
      private static readFileAsDataURL(file: File): Promise<string> {
        return new Promise<string>((resolve, reject) => {
          const reader = new FileReader();
          reader.onload = () => {
            resolve(reader.result as string);
          };
          reader.onerror = () => {
            reject(reader.error);
          };
          reader.readAsDataURL(file);
        });
      }
}
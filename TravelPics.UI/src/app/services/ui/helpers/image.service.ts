import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { DocumentHelper } from 'src/app/shared/helpers/documentHelper';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(
    private sanitizer: DomSanitizer
  ) { }

  public getSanitizedBlobUrlFromBase64(base64: string, fileName: string): any {
    let contentType: string = `image/${DocumentHelper.getDocumentExtension(fileName)}`;
    const byteCharacters = atob(base64);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: contentType });
    const blobUrl = URL.createObjectURL(blob);
    const sanitizedUrl = this.sanitizer.bypassSecurityTrustUrl(blobUrl);
    return sanitizedUrl;
}
}

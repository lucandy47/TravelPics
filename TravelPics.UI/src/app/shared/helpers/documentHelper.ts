export class DocumentHelper{
    public static getDocumentExtension(documentName: string):string{
        return documentName.substring(documentName.lastIndexOf('.') + 1, documentName.length);
    }
}
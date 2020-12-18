export interface Purchase {
    userId: number;
    purchasedMovies: PurchasedMovie[];
  }
  
  interface PurchasedMovie {
    purchaseDateTime: string;
    id: number;
    title: string;
    posterUrl: string;
    releaseDate: string;
  }
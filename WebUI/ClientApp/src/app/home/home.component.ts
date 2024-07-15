import {Component} from '@angular/core';
import {IVideoClient, VideoClient, VideoId, YoutubeClient} from "../web-api-client";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {


    constructor(private client: VideoClient) {
    }

    downloadVideo() {
        this.client.get("https://www.youtube.com/watch?v=6n3pFFPSlW4")
            .subscribe((data) => {
                console.log(data);
            });
    }
}

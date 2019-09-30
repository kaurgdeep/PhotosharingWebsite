import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { PostService } from '../../services/PostService';
import { IPost } from '../../Dtos/Interfaces/IPost';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';
import { IUserInformation } from '../../Dtos/Interfaces/IUserInformation';
import { UserService } from '../../services/UserService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  vm: IPost = {};
  apiCall: boolean;
  public progress: number;
  public message: string;
  me: IUserInformation;

  constructor(private userService: UserService, private postService: PostService,public router: Router, private http: HttpClient) { }

  async ngOnInit() {
    this.me = await this.userService.getMe();
    console.log(this.me);
    if (this.me == null) {
        this.router.navigate(['/login']);
    }
  }

  @Output() postDone = new EventEmitter();
  

  async postClick() {
    this.apiCall = true;
    console.log(this.vm);
    await this.postService.create(this.vm); // POST api/posts
    //this.postDone.emit(this.vm);

    this.vm = { };
    this.apiCall = false;

    
  }

  upload(files) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (let file of files)
      formData.append(file.name, file);

    const uploadReq = new HttpRequest('POST', `/api/upload`, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response)
        this.message = event.body.toString();
        console.log(this.message);
        this.vm.imagePath = this.message;
    });

  }
}

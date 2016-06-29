-- Create table
create table BLOGS
(
  id   NUMBER not null,
  name VARCHAR2(200) not null
);
-- Create/Recreate primary, unique and foreign key constraints 
alter table BLOGS
  add constraint PK_BLOGS primary key (ID);

-- Create table
create table POSTS
(
  id      NUMBER not null,
  blog_id NUMBER not null,
  title   VARCHAR2(200) not null,
  content VARCHAR2(500) not null
);

alter table POSTS
  add constraint PK_POSTS primary key (ID);
alter table POSTS
  add constraint FK_POSTS_BLOG foreign key (BLOG_ID)
  references BLOGS (ID);

declare
  blogId number := 0;
  postId number := 0;
begin
  for x in 1..1000 loop
    blogId := blogId + 1;
    insert into blogs values (blogId, 'Blog ' || blogId);
    for x in 1..5 loop
      postId := postId + 1;
      insert into posts values (postId, blogId, 'Post ' || postId, 'Content for post ' || postId);
    end loop;
  end loop;
  commit;
end;
/